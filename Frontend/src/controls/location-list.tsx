import React, {ChangeEvent, useEffect, useMemo, useState} from 'react';
import {ContactLocation} from "../services/data-objects/contact-location";
import {locationsService} from "../services/locations-service";
import {contactsService} from "../services/contacts-service";

interface locationProps {
onRefreshContacts : () => void;
}

export const LocationList: React.FC<locationProps> = (props: locationProps) =>
{
    const [searchValue, setSearchValue] = useState('');
    const [searchItems, setSearchItems] = React.useState<ContactLocation[]>([]);
    const [locations, setLocations] = useState<ContactLocation[]>([]);
    const service = useMemo(() => new locationsService(), [])
    const contService = useMemo(() => new contactsService(), [])


    useEffect(() => {
        service.getLocations().then((data) => setLocations(data))
    },[service])

    const searchChangedHandler = async (event : ChangeEvent<HTMLInputElement>) =>
    {
        let value = event.target.value;
        setSearchValue(value)
        if (value.trim().length > 2)
        {
            setSearchValue(event.target.value)
                const data = await service.searchLocations(value);
                setSearchItems(data);
        }
        else
        {
            setSearchItems([]);
        }
    }

    const addButtonClickHandler = (item: ContactLocation)=>
    {
        service.addLocation(item)
            .then(() =>
                service.getLocations()
                    .then(data => setLocations(data))
            ).catch((error) => console.log(error));
    }
    const deleteButtonClickHandler = (item: ContactLocation)=>
    {
        service.deleteLocation(item)
            .then(() =>
                service.getLocations()
                    .then(data => setLocations(data))
            ).catch((error) => console.log(error));
    }

    const updateButtonClickHandler = (item: ContactLocation)=>
    {
        contService.updateLocation(item.title)
            .then(() =>
                service.getLocations()
                    .then(data => {
                        setLocations(data);
                        props.onRefreshContacts();
                    })
            ).catch((error) => console.log(error));
    }

    let filteredSearchItems = searchItems.filter(i => locations.findIndex(l => l.title === i.title)<0);
    let searchPopup = filteredSearchItems.length > 0
        ?(
            <ul>
                {filteredSearchItems.map((item) => (
                    <li key = {item.title} className="SearchItem">
                        <div>{item.title}-({item.text})</div>
                        <button onClick={() => addButtonClickHandler(item)}>
                            Add
                        </button>
                    </li>
                ))}
            </ul>
        )
        :(<div>No locations found</div>)

    let locationsList = locations.length === 0
        ? (<p>No locations added.</p>)
        :(
            <ul>
                {locations.map((location) => (
                    <li key={location.id} className="Panel">
                        <div className="LocationHeader">
                            <strong>{location.title}</strong> - ({location.text})
                        </div>
                        <div className="DetailsPanel">
                            <div>
                                <strong>Last updated on: </strong>
                                {location.lastUpdated === null || location.lastUpdated === undefined ? "None"  : location.lastUpdated.toString().substring(0, 16)}
                            </div>
                            <div>
                                <strong>Contacts: </strong>{location.countAll}
                            </div>
                            <div>
                                <strong>New contacts: </strong>{location.countNew}
                            </div>
                        </div>
                        <div style={{ display: "flex", justifyContent: "flex-end" }}>
                            <button onClick={() => {deleteButtonClickHandler(location)}}>
                                Remove
                            </button>
                            <button onClick={() => {updateButtonClickHandler(location)}}>
                                Grab Data
                            </button>
                        </div>
                    </li>
                ))}
            </ul>

        )

    return (
        <div className="Panel">
            <h3 className="PanelHeader">Locations</h3>
            <div className="Panel">
                <div className="SearchItem">
                    <label>Add location</label>
                    <input
                    type="text"
                    value={searchValue}
                    onChange={searchChangedHandler}
                    />
                </div>
                {searchPopup}
            </div>
            <h3 className="PanelHeader">Active Locations</h3>
            {locationsList}
        </div>
    );
}

