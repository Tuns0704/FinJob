import React, { useState } from "react";
import axios from "axios";

function JobForm() {
	const [formData, setFormData] = useState({
		description: "",
		position: "",
		requirement: "",
		location: "",
		benefits: "",
		salary: "",
		companyId: "",
	});

	const handleChange = (event) => {
		setFormData({
			...formData,
			[event.target.name]: event.target.value,
		});
	};

	const handleSubmit = (event) => {
		event.preventDefault();
		axios
			.post("https://localhost:7294/api/jobapi/", formData)
			.then((response) => {
				console.log(response.data);
				// Do something with the response data
			})
			.catch((error) => {
				console.error(error);
			});
	};

	return (
		<form onSubmit={handleSubmit}>
			<label>
				Description:
				<input
					type="text"
					value={formData.description}
					onChange={handleChange}
				/>
			</label>
			<br />
			<label>
				Position:
				<input type="text" value={formData.position} onChange={handleChange} />
			</label>
			<br />
			<label>
				Requirement:
				<input
					type="text"
					value={formData.requirement}
					onChange={handleChange}
				/>
			</label>
			<br />
			<label>
				Location:
				<input type="text" value={formData.location} onChange={handleChange} />
			</label>
			<br />
			<label>
				Benefits:
				<input type="text" value={formData.benefits} onChange={handleChange} />
			</label>
			<br />
			<label>
				Salary:
				<input type="text" value={formData.salary} onChange={handleChange} />
			</label>
			<br />
			<label>
				Company ID:
				<input type="text" value={formData.companyId} onChange={handleChange} />
			</label>
			<br />
			<button type="submit">Submit</button>
		</form>
	);
}

export default JobForm;
