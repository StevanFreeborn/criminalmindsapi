import React from 'react';
import Card from 'react-bootstrap/Card';

export default function CharacterCard(props) {
    
    const characterName = props.character.fullName;
    const image = new URL(props.character.image).pathname;
    
    return(

        <Card style={{ width: '22rem' }} className='p-1 m-3'>
            <Card.Img variant='top' src={image} width='100%' />
            <Card.Body>
                <Card.Title>{characterName}</Card.Title>
                <Card.Text>
                    Some quick example text to build on the card title and make up the
                    bulk of the card's content.
                </Card.Text>
            </Card.Body>
        </Card>

    );
}