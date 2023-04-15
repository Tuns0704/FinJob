import axios from "axios";

export const register = (body) =>
	axios.post(`https://localhost:7294/api/auth/register`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const login = (body) =>
	axios.post(`https://localhost:7294/api/auth/login`, body, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});
