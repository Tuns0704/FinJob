import React from "react";
import { Navbar, CompanyForm, CompanyCard, JobForm, JobCard } from "./components";
import styles from "./styles";

function App() {
	return (
		<div className="App">
			<div className="bg-primary w-full overflow-hidden">
				<div className={`${styles.paddingX} ${styles.flexCenter}`}>
					<div className={`${styles.boxWidth}`}>
						<Navbar />
					</div>
				</div>
				<div className={`bg-primary ${styles.flexStart}`}>
					<div className={`${styles.boxWidth}` }>
						<CompanyCard/>			
						<JobCard/>			
					</div>
				</div>
				<div className={`bg-primary ${styles.paddingX}${styles.flexStart}`}>
					<div className={`${styles.boxWidth}`}>
						<CompanyForm />
						<JobForm />
					</div>
				</div>
			</div>
		</div>
	);
}

export default App;
