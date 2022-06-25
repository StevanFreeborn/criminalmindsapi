import { baseUrl, versionHeader, versionHeaderValue } from '../constants';

class CharacterService {

    getCharacters = async () => {

        const res = await fetch(`${baseUrl}/api/characters`, {
            headers: {
                [versionHeader]: versionHeaderValue
            }
        });

        return res;

    }

}

export default CharacterService;