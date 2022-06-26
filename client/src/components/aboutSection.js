import React from 'react';

import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function AboutSection(props) {
    return(
        <Row className='my-2'>

            <Col>

                <h4 className='highlight'>{props.section.header}</h4>

                <hr />

                <p>{props.section.description}</p>

            </Col>

        </Row>
    );
}