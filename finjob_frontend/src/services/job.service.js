import axios from "axios";

export const getAllJobs = async () =>
	axios.get(`https://localhost:7294/api/JobAPI`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const getJob = (id) =>
	axios.get(`https://localhost:7294/api/JobAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createJob = (body) =>
	axios.post(`https://localhost:7294/api/JobAPI`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const updateJob = (id, body) =>
	axios.put(`https://localhost:7294/api/JobAPI/${id}`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const deleteJob = (id) =>
	axios.delete(`https://localhost:7294/api/JobAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
