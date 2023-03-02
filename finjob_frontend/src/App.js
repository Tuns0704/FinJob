import React, { useState, useEffect } from 'react';

function App() {
  const [companies, setCompanies] = useState([]);

  useEffect(() => {
      fetch('https://localhost:7294/api/companyapi', {
          method: "GET",
          headers: {
              'Content-Type': 'application/json',
              'Access-Control-Allow-Credentials': true
          },

          credentials: "include"
          }) 
      .then(response => response.json())
      .then(data => setCompanies(data))
      .catch(error => console.log(error));
  }, []);

  return (
    <div>
      <h1>List of Companies</h1>
      <ul>
        {companies.map(company => (
          <li key={company.id}>
            {company.statusCode} ({company.description})
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;