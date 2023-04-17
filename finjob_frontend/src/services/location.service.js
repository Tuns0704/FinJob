import axios from "axios";

export const getAllLocations = () =>
	axios.get(`${process.env.REACT_APP_API}/LocationAPI`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const getLocation = (id) =>
	axios.get(`${process.env.REACT_APP_API}/LocationAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createLocation = (body) =>
	axios.post(`${process.env.REACT_APP_API}/LocationAPI`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const updateLocation = (id, body) =>
	axios.put(`${process.env.REACT_APP_API}/LocationAPI/${id}`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const deleteLocation = (id) =>
	axios.delete(`${process.env.REACT_APP_API}/LocationAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
