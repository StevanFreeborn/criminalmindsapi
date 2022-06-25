import React from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';

export default function CardHolder(props) {

    return(
        <Container fluid className="bg-dark">
            <Row className="d-flex flex-row justify-content-center align-items-center py-3">
                {props.children}
            </Row>
        </Container>
    );

}