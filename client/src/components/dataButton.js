import React from 'react';

export default function DataButton(props) {

    const label = props.label;

    return (
        <button
            value={label.toLowerCase()}
            onClick={(e) => props.handleClick(e.target.value)}
        >
            {props.label}
        </button>
    );

}