import React from 'react';
import { Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './styles/app.css';

import NavBar from './components/navbar';
import Home from './pages/home';
import About from './pages/about';
import Footer from './components/footer';

export default function App() {
    return (
        <div className='d-flex flex-column min-vh-100'>
            <NavBar/>
            <Routes>
                <Route path='/' element={<Home/>}/>
                <Route path='/About' element={<About/>}/>
            </Routes>
            <Footer/>
        </div>
    );
}
