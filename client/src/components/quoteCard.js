import React from 'react';
import Card from 'react-bootstrap/Card';

export default function QuoteCard(props) {

    const text = props.quote.text;
    const source = props.quote.source

    return (
        <Card style={{ width: '22rem' }} className='p-1 m-3'>

            <Card.Body>

                <Card.Text>{text}</Card.Text>
                <Card.Text>{source}</Card.Text>

            </Card.Body>

        </Card>
    );
}