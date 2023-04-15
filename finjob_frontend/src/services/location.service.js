import axios from "axios";

export const getAllLocations = () =>
	axios.get(`https://localhost:7294/api/LocationAPI`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const getLocation = (id) =>
	axios.get(`https://localhost:7294/api/LocationAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createLocation = (body) =>
	axios.post(`https://localhost:7294/api/LocationAPI`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const updateLocation = (id, body) =>
	axios.put(`https://localhost:7294/api/LocationAPI/${id}`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const deleteLocation = (id) =>
	axios.delete(`https://localhost:7294/api/LocationAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
