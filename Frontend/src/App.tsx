import React from 'react';
import './App.css';
import {LocationList} from "./controls/location-list";
import {ContactList} from "./controls/contact-list";

function App() {
    const [flag, setFlag] = React.useState(false);
    const onRefreshContacts = () => {
        setFlag(!flag);
    };
  return (
    <div className="App">
        <div className="Panel">
            <h2>Contacts grab and search demo</h2>
        </div>
        <div className="Container">
            <LocationList onRefreshContacts={onRefreshContacts} />
            <ContactList flag = {flag}/>
        </div>
    </div>
  );
}

export default App;
