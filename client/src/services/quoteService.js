import { baseUrl, versionHeader, versionHeaderValue } from '../constants';

class QuoteService {

    getQuotes = async () => {

        const res = await fetch(`${baseUrl}/api/quotes`, {
            headers: {
                [versionHeader]: versionHeaderValue
            }
        });

        return res;

    }

}

export default QuoteService;