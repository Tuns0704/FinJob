/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ["./index.html", "./src/**/*.{js,jsx}"],
	theme: {
		extend: {
			fontFamily: {
				poppins: ["Poppins", "sans-serif"],
			},
			colors: {
				primary: "#EE786B",
				secondary: "#32A3BB",
				lightOrange: "#ffeeea",
				dark: "#494544",
				white: "#ffffff",
				light: "#F8FAFC",
				lightTurquoise: "#F4FAFC",
				dimBlue: "rgba(9, 151, 124, 0.1)",
			},
		},
		screens: {
			xs: "480px",
			ss: "620px",
			sm: "768px",
			md: "1060px",
			lg: "1200px",
			xl: "1700px",
		},
	},
	plugins: [],
};
