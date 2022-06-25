import React from 'react';
import { Github } from 'react-bootstrap-icons';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function Footer() {
    return (
        <footer className='mt-auto py-3'>
            <Container>

                <hr/>
            
                <Row>

                    <Col className='text-start'>
                        <span>
                            <span className='highlight'>criminal</span>mindsapi &#169; 2022
                        </span>
                    </Col>

                    <Col className='text-end'>
                        <a
                            className='text-dark'
                            href='https://github.com/StevanFreeborn/criminalmindsapi'
                            target='_blank'
                            rel="noreferrer"
                        >
                            <Github size={25} />
                        </a>
                    </Col>

                </Row>

            </Container>
        </footer>
    )
}