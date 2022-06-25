const baseUrl = process.env.NODE_ENV === 'development' ?
    '' :
    'https://criminalmindsapi.azurewebsites.net';

const versionHeader = 'x-api-version';
const versionHeaderValue = 1;

export {
    baseUrl,
    versionHeader,
    versionHeaderValue
};
