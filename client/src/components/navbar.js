import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import { NavLink } from 'react-router-dom';
import Container from 'react-bootstrap/Container';

export default function NavBar() {

    return (
        <Navbar
            expand='sm'
            variant='light'
        >
            <Container fluid>

                <Navbar.Brand
                    href='/'
                    className='pt-0'
                >
                    <span className='highlight'>criminal</span>mindsapi
                </Navbar.Brand>

                <Navbar.Toggle aria-controls='basic-navbar-nav'/>
                
                <Navbar.Collapse>

                    <Nav className='ms-auto'>

                        <NavLink
                            to='/'
                            className='nav-link link-dark'
                        >
                            Home
                        </NavLink>

                        <NavLink
                            to='/About'
                            className='nav-link link-dark'
                        >
                            About
                        </NavLink>

                        <a
                            href='https://criminalmindsapi.azurewebsites.net'
                            rel='noreferrer'
                            target='_blank'
                            className='nav-link link-dark'
                        >
                            Documentation
                        </a>

                    </Nav>

                </Navbar.Collapse>

            </Container>
            
        </Navbar>
    )

}