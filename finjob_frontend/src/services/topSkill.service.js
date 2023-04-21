import axios from "axios";
import { config } from "./config";

export const getAllTopSkills = (token) =>
	axios.get(`${process.env.REACT_APP_API}/TopSkillAPI`, config(token));

export const getAllTopSkillsByUserId = (token, userId) =>
	axios.get(
		`${process.env.REACT_APP_API}/TopSkillAPI/${userId}`,
		config(token)
	);

export const getTopSkillById = (token, id) =>
	axios.get(`${process.env.REACT_APP_API}/TopSkillAPI/${id}`, config(token));

export const deleteTopSkill = (token, id) =>
	axios.delete(`${process.env.REACT_APP_API}/TopSkillAPI/${id}`, config(token));

export const createTopSkill = (token, body) =>
	axios.post(`${process.env.REACT_APP_API}/TopSkillAPI`, body, config(token));

export const updateTopSkill = (token, id, body) =>
	axios.put(
		`${process.env.REACT_APP_API}/TopSkillAPI/${id}`,
		body,
		config(token)
	);
