import axios from "axios";

export const getAllPositions = () =>
	axios.get(`https://localhost:7294/api/PositionAPI`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const getPosition = (id) =>
	axios.get(`https://localhost:7294/api/PositionAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createPosition = (body) =>
	axios.post(`https://localhost:7294/api/PositionAPI`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const updatePosition = (id, body) =>
	axios.put(`https://localhost:7294/api/PositionAPI/${id}`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const deletePosition = (id) =>
	axios.delete(`https://localhost:7294/api/PositionAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
