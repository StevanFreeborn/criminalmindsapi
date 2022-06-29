import React from 'react';

import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function LoadingError(props) {
    return (
        <Container className='my-auto'>

            <Row>

                <Col className='text-center'>

                    <p>{props.message}</p>

                    <button
                        className='btn btn-cta'
                        onClick={props.handleClick}
                    >
                        Try again
                    </button>

                </Col>

            </Row>

        </Container>
    );
}