import React from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { TailSpin } from 'react-loading-icons';

export default function Loading() {

    return (
        <Container className='m-auto'>
            <Row>
                <Col className='text-center'>
                    <TailSpin
                        height='3em'
                        fill='#912718'
                        stroke='#912718'
                        speed={1}
                        fillOpacity={1}
                        strokeOpacity={1}
                        strokeWidth={2}
                    />
                     <div className='mt-2'>Loading...</div>
                </Col>
            </Row>
        </Container>
    );

}