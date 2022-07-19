import React from 'react';
import Card from 'react-bootstrap/Card';
import Table from 'react-bootstrap/Table';

export default function CharacterCard(props) {

    const image = new URL(props.character.image).pathname;
    const characterName = props.character.fullName;
    const firstEpisode = props.character.firstEpisode;
    const lastEpisode = props.character.lastEpisode;
    
    return(
        <Card style={{ width: '22rem' }} className='p-1 m-3'>
            
            <Card.Img variant='top' src={image} width='100%' />
            
            <Card.Body>
                
                <Card.Title>{characterName}</Card.Title>
                    
                <Table>
                    
                    <tbody>
                        
                        <tr>
                            
                            <td className='fw-bold'>First Episode:</td>
                            
                            <td>{firstEpisode}</td>
                        
                        </tr>
                        
                        <tr>
                            
                            <td className='fw-bold'>Last Episode:</td>
                            
                            <td>{lastEpisode}</td>
                        
                        </tr>
                    
                    </tbody>
                
                </Table>
            
            </Card.Body>
        
        </Card>
    );
}