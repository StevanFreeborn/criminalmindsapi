import React from 'react';

import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import AboutSection from '../components/aboutSection';
import { aboutSections as sections } from '../constants';

export default function About() {

    const getSections = () => {

        return sections.map(section => {

            return <AboutSection section={section}/>

        });

    }

    return (

        <Container>

            <Row>

                <Col>

                    <h1 className='text-center py-4'>Wanna know more?</h1>

                </Col>

            </Row>

            {getSections()}

        </Container>

    );
}