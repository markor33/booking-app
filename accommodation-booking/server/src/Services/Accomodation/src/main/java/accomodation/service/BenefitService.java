package accomodation.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import accomodation.model.Benefit;
import accomodation.repository.BenefitRepository;

@Service
public class BenefitService {

	@Autowired
	BenefitRepository benefitRepository;
	
	public List<Benefit> findAll() {
		return benefitRepository.findAll();
	}
	
}
