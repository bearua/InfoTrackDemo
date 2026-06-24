import React from 'react';

interface ratingProps {
    rating: number;
    votes: number;
}

export const RatingControl: React.FC<ratingProps> = (props: ratingProps)=>
{
    const range = Array.from({ length: 5 }, (_, index) => index + 1);
    if (props.votes === 0) {
        return (<div></div>);
    }
return (
     <div>
        <div className="RatingContainer">
            {range.map((value) =>
                (
                    <div key={value}
                         className={"Rating Star" + (value * 2 <= props.rating ? "Full" : ((value) * 2 - 1 > props.rating) ? "None" : "Half")}>
                    </div>
                ))}
        </div>
        <div>
            ({props.votes})
        </div>
    </div>
    );
}