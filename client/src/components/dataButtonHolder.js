import React from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';

export default function DataButtonHolder(props) {
    return(
        <Container fluid>

            <Row className="d-flex flex-row justify-content-center align-items-center">
                
                {props.children}
            
            </Row>

        </Container>
    );
}