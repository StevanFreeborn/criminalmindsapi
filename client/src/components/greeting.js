import React from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function Greeting() {

    return (
        <Container>

            <Row className='py-5'>

                <Col className='text-center'>

                    <h1>
                        The <span className='highlight'>Criminal</span> Minds API
                    </h1>

                    <p>
                        Checkout the <a href='https://criminalmindsapi.azurewebsites.net' rel='noreferrer' target='_blank' className='highlight'>documentation</a>.
                    </p>

                </Col>

            </Row>
            
        </Container>
    );

}