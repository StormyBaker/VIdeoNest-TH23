import { Button } from "primereact/button";
import { Link } from "react-router-dom";

export function NestCardFooter(props) {
    return (
        <Link to={`/app/nests/${props.guid}`}>
            <Button label="View Nest" />
        </Link>
    )
}