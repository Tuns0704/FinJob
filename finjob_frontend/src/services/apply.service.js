import axios from "axios";
import { config } from "./config";

export const getAllApplyJob = async (currentPage, searchParam, token) =>
	axios.get(
		`${process.env.REACT_APP_API}/ApplyJobAPI?pageSize=10&pageNumber=${currentPage}`,
		config(token)
	);
export const getApplyJobById = (id, token) =>
	axios.get(`${process.env.REACT_APP_API}/ApplyJobAPI/${id}`, config(token));

export const applyJob = (body, token) =>
	axios.post(`${process.env.REACT_APP_API}/ApplyJobAPI`, body, config(token));

export const deleteJobApply = (id, token) =>
	axios.delete(`${process.env.REACT_APP_API}/ApplyJobAPI/${id}`, config(token));
