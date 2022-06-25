import React from "react";
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import { NavLink } from 'react-router-dom';
import Container from 'react-bootstrap/Container';

export default function NavBar() {

    return (
        <Navbar
            expand="sm" 
            variant="light"
        >
            <Container fluid>
                <Navbar.Brand>
                    <NavLink
                        to='/'
                        className="nav-link link-dark ps-0"
                    >
                        criminalmindsapi
                    </NavLink>
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                <Navbar.Collapse>
                    <Nav>
                        <NavLink
                            to='/About'
                            className="nav-link link-dark"
                        >
                            About
                        </NavLink>
                        <a
                            href="https://criminalmindsapi.azurewebsites.net"
                            rel='noreferrer'
                            target='_blank'
                            className="nav-link link-dark"
                        >
                            Documentation
                        </a>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    )

}