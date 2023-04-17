import axios from "axios";

export const getAllJobs = async () =>
	axios.get(`${process.env.REACT_APP_API}/JobAPI`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const getJob = (id) =>
	axios.get(`${process.env.REACT_APP_API}/JobAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createJob = (body) =>
	axios.post(`${process.env.REACT_APP_API}/JobAPI`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const updateJob = (id, body) =>
	axios.put(`${process.env.REACT_APP_API}/JobAPI/${id}`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const deleteJob = (id) =>
	axios.delete(`${process.env.REACT_APP_API}/JobAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
