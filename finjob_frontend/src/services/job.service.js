import axios from "axios";
import { config } from "./config";

export const getAllJobs = async (currentPage, searchParam, token) =>
	axios.get(
		`${process.env.REACT_APP_API}/JobAPI?pageSize=10&pageNumber=${currentPage}`,
		config(token)
	);

export const getJob = (id) =>
	axios.get(`${process.env.REACT_APP_API}/JobAPI/${id}`, {
		headers: {
			"Content-Type": "application/json",
		},
		validateStatus: (status) => status <= 500,
	});

export const createJob = async (body, token) =>
	axios.post(`${process.env.REACT_APP_API}/JobAPI`, body, config(token));

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
