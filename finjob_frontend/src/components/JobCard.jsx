import React, { useEffect, useState } from 'react';
import axios from 'axios';

function JobCard() {
    const [jobs, setJobs] = useState([]);

    useEffect(() => {
        axios
            .get('https://localhost:7294/api/jobapi/')
            .then((response) => {
                setJobs(response.data.result);
            })
            .catch((error) => {
                console.error(error);
            });
    }, []);

    return (
        <div className="max-w-sm rounded flex">
            {jobs.map((job) => (
                <div key={job.id}>
                    <div className="px-6 py-4">
                        <div className="font-bold text-xl mb-2">{job.description}</div>
                        <p className="text-gray-700 text-base">{job.position}</p>
                        <p className="text-gray-700 text-base">{job.requirement}</p>
                        <p className="text-gray-700 text-base">{job.benefits}</p>
                        <p className="text-gray-700 text-base">{job.salary}</p>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default JobCard;