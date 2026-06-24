import React, {useEffect, useMemo, useState} from "react";
import {Contact} from "../services/data-objects/contact";
import {contactsService} from "../services/contacts-service";
import {RatingControl} from "./rating-control";

interface contactProps {
    flag:boolean;
}
export const ContactList : React.FC<contactProps> = (props) => {
    const [contacts, setContacts] = useState<Contact[]>([]);
    const [filteredContacts, setFilteredContacts] = useState<Contact[]>([]);
    const [nameFilter, setNameFilter] = useState("");
    const [locationFilter, setLocationFilter] = useState("");
    const [phoneFilter, setPhoneFilter] = useState("");
    const [addressFilter, setAddressFilter] = useState("");
    const service = useMemo(() => new contactsService(), [])

    useEffect(() => {
        service.getContacts().then((data) => setContacts(data))
    }, [service, props.flag])
    useEffect(() => {
        let data = contacts.filter((c) => {return nameFilter === "" || c.name?.toLowerCase().includes(nameFilter.toLowerCase())})
        data = data.filter((c) => {return locationFilter === "" || c.location?.toLowerCase().includes(locationFilter.toLowerCase())})
        data = data.filter((c) => {return phoneFilter === "" || c.phone?.toLowerCase().includes(phoneFilter.toLowerCase())})
        data = data.filter((c) => {return addressFilter === "" || c.address?.toLowerCase().includes(addressFilter.toLowerCase())})
        setFilteredContacts(data);
    }, [contacts, nameFilter, locationFilter, phoneFilter, addressFilter]);

    let items = contacts.length === 0
        ? (
            <div>
                No contacts found
            </div>
        )
        : (

            <ul style={{ textAlign: "left", boxSizing: "border-box" }}>
                <li>
                    <strong>
                        {`Showing ${filteredContacts.length} of ${contacts.length} contacts.`}
                    </strong>
                </li>

                <li className="ContactItem" key={-1}>
                    <div className="GridCell" style={{ width: "300px"}}>
                        <input
                            style={{ width: "100%", margin: "3"}}
                            type="text"
                            value={nameFilter}
                            onChange={(event) => {setNameFilter(event.target.value)}}
                        />
                    </div>
                    <div className="GridCell" style={{ width: "150px"}}>

                    </div>
                    <div className="GridCell" style={{ width: "150px"}}>
                        <input
                            style={{width: "100%", margin: "3" }}
                            type="text"
                            value={phoneFilter}
                            onChange={(event) => {setPhoneFilter(event.target.value)}}
                        />
                    </div>
                    <div className="GridCell" style={{ width: "150px"}}>
                        <input
                            style={{ width: "100%", margin: "3" }}
                            type="text"
                            value={locationFilter}
                            onChange={(event) => {setLocationFilter(event.target.value)}}
                        />
                    </div>
                    <div className="GridCell" style={{ width: "100%"}}>
                        <input
                            style={{ width: "100%", margin: "3" }}
                            type="text"
                            value={addressFilter}
                            onChange={(event) => {setAddressFilter(event.target.value)}}
                        />
                    </div>
                </li>
                <li className="ContactItem" key={0} style={{  background: "darkolivegreen" }}>
                    <div className="GridCell" style={{ width: "300px" }}>
                        Name
                    </div>
                    <div className="GridCell" style={{ width: "150px"}}>
                        Rating
                    </div>
                    <div className="GridCell" style={{ width: "150px" }}>
                        Phone
                    </div>
                    <div className="GridCell" style={{ width: "150px" }}>
                        Location
                    </div>
                    <div className="GridCell" style={{ width: "100%" }}>
                        Address
                    </div>
                </li>
                {filteredContacts.map((item) =>
                (
                    <li className="ContactItem" key={item.id}>
                        <div className="GridCell" style={{ width: "300px" }}>
                            {item.name}
                            {item.isNew ? <strong>(New)</strong> : <span/>}
                        </div>
                        <div className="GridCell" style={{ width: "150px"}}>
                            <RatingControl rating={item.starsCount??0} votes={item.votesCount??0} />
                        </div>
                        <div className="GridCell" style={{ width: "150px" }}>
                            {item.phone}
                        </div>
                        <div className="GridCell" style={{ width: "150px" }}>
                            {item.location}
                        </div>
                        <div className="GridCell" style={{ width: "100%" }}>
                            {item.address}
                        </div>
                    </li>
                )
            )}

            </ul>
        );

    return (
        <div className="Panel ContactsPanel">
            <div className="PanelHeader">
                <h3>
                    Contacts
                </h3>
            </div>
            {items}
        </div>
    );
}