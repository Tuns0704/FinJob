import axios from "axios";
import { config } from "./config";

export const getAllUser = async (currentPage, searchParam, token) =>
	axios.get(
		`${process.env.REACT_APP_API}/UserAPI?pageSize=10&pageNumber=${currentPage}`,
		config(token)
	);

export const getUserById = (id, token) =>
	axios.get(`${process.env.REACT_APP_API}/UserAPI/${id}`, config(token));

export const createUser = async (body, token) =>
	axios.post(`${process.env.REACT_APP_API}/auth/register`, body, config(token));

export const updateUser = (id, body, token) =>
	axios.put(`${process.env.REACT_APP_API}/UserAPI/${id}`, body, config(token));

export const deleteUser = (userId, token) =>
	axios.delete(`${process.env.REACT_APP_API}/UserAPI/${userId}`, config(token));
