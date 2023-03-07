import React, { useEffect, useState } from 'react';
import axios from 'axios';

function CompanyCard() {
    const [companies, setCompanies] = useState([]);

    useEffect(() => {
        axios
            .get('https://localhost:7294/api/companyapi/')
            .then((response) => {
                setCompanies(response.data.result);
            })
            .catch((error) => {
                console.error(error);
            });
    }, []);

    return (
        <div className="max-w-sm rounded flex">
            {companies.map((company) => (
                <div key={company.id}>
                    <img className="w-full" src={company.imageURL} alt={company.name} />
                    <div className="px-6 py-4">
                        <div className="font-bold text-xl mb-2">{company.name}</div>
                        <p className="text-gray-700 text-base">{company.description}</p>
                        <p className="text-gray-700 text-base">{company.scale}</p>
                        <p className="text-gray-700 text-base">{company.location}</p>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default CompanyCard;