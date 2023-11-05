export function StatCard(props) {
    return (
        <div className="surface-0 shadow-2 p-3 border-1 border-50 border-round">
            <div className="flex justify-content-between mb-3">
                <div>
                    <span className="block text-500 font-medium mb-3">{props.title}</span>
                    <div className="text-900 font-medium text-xl">{props.description}</div>
                </div>
                <div className="flex align-items-center justify-content-center border-round" style={{ width: '2.5rem', height: '2.5rem' }}>
                    {props.icon}
                </div>
            </div>
        </div>
    )
}