const baseUrl = process.env.NODE_ENV === 'development' ?
    '' :
    'https://criminalmindsapi.azurewebsites.net';

const versionHeader = 'x-api-version';
const versionHeaderValue = 1;

const aboutSections = [
    {
      header: "Who",
      description: "Hi, I'm Stevan Freeborn. I am well...someone who likes to make things and put them on the interwebz. One of my favorite tv shows is Criminal Minds."  
    },
    {
        header: "What",
        description: "The Criminal Minds API is a collection of information about the Criminal Minds series. This information is organized into Characters, Episodes, Quotes, and Seasons. The API has documentation that is programmatically generated using swagger and can be used to make exploratory requests."  
    },
    {
        header: "Why",
        description: "I've been learning how to code and wanted to build a web api using MongoDB, ASP.NET Core 6, and React and I figured what better subject matter than one of my favorite tv shows."  
    },
    {
        header: "Issues",
        description: "If you come across any errors, missing information, or would like to suggest changes, please contribute by creating an issue or a pull request on GitHub."  
    },
    {
        header: "Contact",
        description: "If you would like to talk about the project or discuss the finer points of brewing a great cup of coffee, send me an email at stevan.freeborn@gmail.com. I would love to chat with you."  
    },
    {
        header: "License",
        description: "Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE."  
    }
];

export {
    baseUrl,
    versionHeader,
    versionHeaderValue,
    aboutSections
};
