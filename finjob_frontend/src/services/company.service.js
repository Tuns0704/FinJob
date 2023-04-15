import axios from "axios";

export const getAllCompanys = async () =>
	axios.get(`https://localhost:7294/api/CompanyAPI`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const getCompany = (id) =>
	axios.get(`https://localhost:7294/api/CompanyAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createCompany = (body) =>
	axios.post(`https://localhost:7294/api/CompanyAPI`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const updateCompany = (id, body) =>
	axios.put(`https://localhost:7294/api/CompanyAPI/${id}`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const deleteCompany = async (id) =>
	axios.delete(`https://localhost:7294/api/CompanyAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
