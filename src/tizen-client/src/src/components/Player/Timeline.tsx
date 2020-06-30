import * as React from "react";
import './Timeline.styl'

export interface TimelineProps {
    progressPercentage: number
}

export const Timeline = (props: TimelineProps) => {
    console.log(props.progressPercentage)
    return <div className='timeline__container' tabIndex={0}>
        <div className='timeline__passed' style={{
            width: `${props.progressPercentage}%`
        }} />
        <div className='timeline__rest' style={{
            width: `${100 - props.progressPercentage}%`
        }}/>
    </div>
}