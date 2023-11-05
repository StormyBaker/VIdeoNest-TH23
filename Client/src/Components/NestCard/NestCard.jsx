import { Card } from "primereact/card";

export function NestCard(props) {
    return (
        <Card title={props.title} subTitle={props.subtitle} footer={props.footer} className="col-12 md:col-3">
        </Card>
    )
}