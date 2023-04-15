export const config = (token) => ({
	headers: {
		"Content-Type": "application/json",
		Authorization: `Bearer ${token}`,
	},
	validateStatus: (statusCode) => statusCode <= 500,
});
