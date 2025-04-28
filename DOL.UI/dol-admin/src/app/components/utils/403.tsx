import React from 'react'
import { Result } from 'antd';

class AccessDenied extends React.Component {
    render() {
        return (
            <Result
                status="403"
                title="403"
                subTitle="Sorry, you are not authorized to access this page."
            />
        );
    }
}
export default AccessDenied;