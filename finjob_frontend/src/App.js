import React, { Component } from 'react';

export default class App extends Component {
    static displayName = App.name;

    render() {
        const { DataisLoaded, items } = this.state;
        if (!DataisLoaded) return <div>
            <h1> Pleses wait some time.... </h1> </div>;
        return (
            <div>
                <h1>FinJob</h1>
                {
                items.map((item) => (
                <ol key={item.id} >
                    Name: {item.name},
                    Descriotion: {item.description},
                    Scale: {item.scale}
                    Location: {item.location}
                </ol>
                ))}
                
            </div>
        );
    }
}
