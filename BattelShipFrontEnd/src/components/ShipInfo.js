import React from 'react';

const ShipInfo = ({ ships, title }) => (
    <div className="ship-info">
        <h3>{title}</h3>
        <ul>
            {ships.map((ship, index) => (
                <li key={index}>
                    {ship.name} - {ship.sunkSize}/{ship.size} hits ({ship.sunkPercentage}% sunk) {ship.isSunk ? "(Sunk)" : ""}
                </li>
            ))}
        </ul>
    </div>
);

export default ShipInfo;
