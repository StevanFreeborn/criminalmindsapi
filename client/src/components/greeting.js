import React from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function Greeting() {

    return (
        <Container>

            <Row className='py-4'>

                <Col>

                    <h1 className='text-center mb-3'>
                        The <span className='highlight'>Criminal</span> Minds API
                    </h1>

                    <p>
                        Welcome to the Criminal Minds API! The documentation should supply you with all the information you need to start making requests. It is programmatically generated using swagger and provides the ability to make exploratory calls easily. Give it a review before you get started on your project.
                    </p>

                    <p>
                        Note the api is free, but to prevent malicious activity there is a rate limit of 10,000 requests a day. If that limit is reached you will receive a response with a 429 status code. Details about your rate limit standing are returned in response headers, specifically the x-rate-limit-limit, x-rate-limit-remaining, and x-rate-limit-reset headers.
                    </p>

                    <p className='text-center'>
                        Checkout the <a href='https://criminalmindsapi.azurewebsites.net' rel='noreferrer' target='_blank' className='highlight'>documentation</a>.
                    </p>

                </Col>

            </Row>
            
        </Container>
    );

}