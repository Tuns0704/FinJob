import axios from "axios";

export const getAllPositions = () =>
	axios.get(`${process.env.REACT_APP_API}/PositionAPI`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const getPosition = (id) =>
	axios.get(`${process.env.REACT_APP_API}/PositionAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createPosition = (body) =>
	axios.post(`${process.env.REACT_APP_API}/PositionAPI`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const updatePosition = (id, body) =>
	axios.put(`${process.env.REACT_APP_API}/PositionAPI/${id}`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const deletePosition = (id) =>
	axios.delete(`${process.env.REACT_APP_API}/PositionAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
