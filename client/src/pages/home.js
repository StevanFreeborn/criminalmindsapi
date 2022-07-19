import React, { useEffect, useState } from 'react';

import Greeting from '../components/greeting';
import CardHolder from '../components/cardHolder';
import CharacterCard from '../components/characterCard';
import Loading from '../components/loading';
import LoadingError from '../components/loadingError';
import DataButtonHolder from '../components/dataButtonHolder';
import DataButton from '../components/dataButton';

import CharacterService from '../services/characterService';
import QuoteService from '../services/quoteService';
import QuoteCard from '../components/quoteCard';
const characterService = new CharacterService();
const quoteService = new QuoteService();

export default function Home() {

    const [currentData, setCurrentData] = useState('characters');
    const [data, setData] = useState([]);
    const [errorMessage, setErrorMessage] = useState(null);

    const getCharacters = async () => {

        setData([]);
        setCurrentData('characters');

        const res = await characterService.getCharacters();

        if (!res.ok) {

            return setErrorMessage(`Failed to load characters`);

        }

        const characters = await res.json();

        return setData(characters);
    }

    const getQuotes = async () => {

        setData([]);
        setCurrentData('quotes');

        const res = await quoteService.getQuotes();

        if (!res.ok) {

            return setErrorMessage(`Failed to load quotes`);

        }

        const quotes = await res.json();

        return setData(quotes.slice(0,20));
    }

    const getEpisodes = () => {console.log('episodes')}

    const getSeasons = () => {console.log('seasons')}

    useEffect(() => {

        getCharacters();

    }, []);

    const models = [{ id: 1, name: 'Characters', handleClick: getCharacters},
                    { id: 2, name: 'Quotes', handleClick: getQuotes },
                    { id: 3, name: 'Episodes', handleClick: getEpisodes },
                    { id: 4, name: 'Seasons', handleClick: getSeasons }];

    const getDataButtons = () => {

        return models.map(model => {

            return (
                <DataButton
                    label={model.name}
                    handleClick={model.handleClick}
                    key={model.id}
                />
            );

        });

    }

    const getCards = () => {

        if (currentData === 'quotes') {

            return data.map(quote => {

                return (

                    <QuoteCard
                        quote={quote}
                        key={quote.id}
                    />
                );

            });

        }

        return data.map(character => {

            return (

                <CharacterCard
                    character={character}
                    key={character.id}
                />

            );

        })

    }

    const handleTryAgain = async () => {

        setErrorMessage(null);

    }

    return (
        <>
            <Greeting />

            <DataButtonHolder>
                {getDataButtons()}
            </DataButtonHolder>

            {data.length > 0 ?

                <CardHolder>

                    {getCards()}

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