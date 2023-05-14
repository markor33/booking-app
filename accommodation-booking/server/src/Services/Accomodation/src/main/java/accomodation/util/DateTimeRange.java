package accomodation.util;

import java.time.LocalDateTime;
import java.util.Objects;

import javax.persistence.Embeddable;

@Embeddable
public class DateTimeRange {
	
	private LocalDateTime start;
	
	private LocalDateTime end;
	
	public DateTimeRange() {
		
	}
	
	public DateTimeRange(LocalDateTime start, LocalDateTime end) {
		super();
		this.start = start;
		this.end = end;
	}
	
	public DateTimeRange(DateTimeRange d) {
		super();
		this.start = d.getStart();
		this.end = d.getEnd();
	}

	public boolean overlapsWith(DateTimeRange dateRange) { 
		return (dateRange.start.compareTo(start) >= 0 && dateRange.start.compareTo(end) <= 0) || 
			(dateRange.end.compareTo(start) >= 0 && dateRange.end.compareTo(end) <= 0) ||
			(dateRange.start.compareTo(start) <= 0 && dateRange.end.compareTo(end) >= 0);
	}
	 
	public LocalDateTime getStart() {
		return start;
	}

	public void setStart(LocalDateTime start) {
		this.start = start;
	}

	public LocalDateTime getEnd() {
		return end;
	}

	public void setEnd(LocalDateTime end) {
		this.end = end;
	}

	@Override
	public String toString() {
		return "DateTimeRange [start=" + start + ", end=" + end + "]";
	}

	@Override
	public int hashCode() {
		return Objects.hash(end, start);
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		DateTimeRange other = (DateTimeRange) obj;
		return Objects.equals(end, other.end) && Objects.equals(start, other.start);
	}
	
}