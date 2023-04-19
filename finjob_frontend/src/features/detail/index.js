import { useCallback, useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { authContext } from "../../cores/context/auth";
import { getJob } from "../../services/job.service";
import { toast } from "react-toastify";

const JobDetail = () => {
	const { id } = useParams();
	const navigate = useNavigate();

	const [job, setJob] = useState({});

	// const {
	// 	state: { token },
	// } = useContext(authContext);

	const getPostById = useCallback(async () => {
		try {
			const response = await getJob(id);
			if (response.status === 200) {
				setJob(response.data.result);
			}
		} catch (error) {
			console.log(error);
		}
	}, [id]);

	useEffect(() => {
		getPostById();
	}, [getPostById]);

	return (
		<div>
			<h1>Job Detail</h1>
			<div className="row">
				<div className="col-md-8">
					<div className="card">
						<div className="card-body">
							<h5 className="card-title">{job.title}</h5>
							<p className="card-text">{job.description}</p>
							<p className="card-text">{job.salary}</p>
							{job.locations?.map((location) => (
								<p key={location} className="text-gray-900 whitespace-no-wrap">
									{location}
								</p>
							))}
							{job.positions?.map((position) => (
								<p key={position} className="text-gray-900 whitespace-no-wrap">
									{position}
								</p>
							))}
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default JobDetail;
