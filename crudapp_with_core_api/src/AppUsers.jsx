import { useEffect, useState } from "react";

function AppUsers() {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        fetch("https://localhost:7297/api/AppUsers")
            .then(res => res.json())
            .then(data => setUsers(data))
            .catch(err => alert(err.message));
    }, []);

    return (
        <div>
            <h1>App Users List</h1>
            <br />
            <table border="1" cellPadding="5" cellSpacing="0">
                <thead style={{backgroundColor:"grey", color:"White"}}>
                    {users.length > 0 &&
                        Object.keys(users[0]).map(key => (
                            <th key={key}>{key}</th>
                        ))}
                </thead>
                <tbody>
                    {users.map((user, index) => (
                        <tr key={index}>
                            {Object.values(user).map((val, i) => (
                                <td key={i}>{val}</td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>

        </div>
    )
}

export default AppUsers;