import axios from "axios";

export const getAllCompanys = async () =>
	axios.get(`${process.env.REACT_APP_API}/CompanyAPI`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const getCompany = (id) =>
	axios.get(`${process.env.REACT_APP_API}/CompanyAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createCompany = (body) =>
	axios.post(`${process.env.REACT_APP_API}/CompanyAPI`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const updateCompany = (id, body) =>
	axios.put(`${process.env.REACT_APP_API}/CompanyAPI/${id}`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const deleteCompany = async (id) =>
	axios.delete(`${process.env.REACT_APP_API}/CompanyAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
