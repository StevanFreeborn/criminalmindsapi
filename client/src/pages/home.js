import React, { useEffect, useState } from 'react';

import Greeting from '../components/greeting';
import CardHolder from '../components/cardHolder';
import CharacterCard from '../components/characterCard';
import Loading from '../components/loading';
import LoadingError from '../components/loadingError';

import CharacterService from '../services/characterService';
const characterService = new CharacterService();

export default function Home() {

    const [characters, setCharacters] = useState([]);
    const [errorMessage, setErrorMessage] = useState(null);

    const getCharacters = async () => {

        const res = await characterService.getCharacters();
        
        if (!res.ok) {

            return setErrorMessage('Failed to load characters.');

        }

        const characters = await res.json();

        return setCharacters(characters);

    }

    useEffect(() => {

        getCharacters();

    }, []);

    const getCharacterCards = () => {

        return characters.map(character => {

            return (

                <CharacterCard 
                    character={character}
                    key={character.id}
                />

            );

        })

    }

    const handleTryAgain = async () => {

        if (errorMessage === 'Failed to load characters.') return await getCharacters();

    }

    return (
        <>
            <Greeting />

            {characters.length > 0 ?

                <CardHolder>

                    {getCharacterCards()}

                </CardHolder>
                
                : errorMessage ? 

                <LoadingError
                    message={errorMessage}
                    handleClick={handleTryAgain}
                />
                
                : <Loading />}
        </>
    );
}