import React, { useEffect, useState } from 'react';

import Greeting from '../components/greeting';
import CardHolder from '../components/cardHolder';
import CharacterCard from '../components/characterCard';

import CharacterService from '../services/characterService';
const characterService = new CharacterService();

export default function Home() {

    const [characters, setCharacters] = useState([]);

    const getCharacters = async () => {

        const res = await characterService.getCharacters();
        
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
                />

            );

        })

    }

    return (
        <>
            {characters.length > 0 ?
                <>
                    <Greeting />
                    <CardHolder>
                        {getCharacterCards()}
                    </CardHolder>
                </>
            : null}
        </>
    );
}