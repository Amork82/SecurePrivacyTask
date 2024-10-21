import { Component, OnInit } from '@angular/core';
import { BaseService } from '../services/base.service';
import { BinaryResult } from '../models/binary-result.model'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-binary-check',
  templateUrl: './binary-check.component.html',
  styleUrl: './binary-check.component.css'
})

export class BinaryCheckComponent {
  verifyForm: FormGroup;
  binaryResult: BinaryResult | null = null; // Memorizza il risultato della verifica

  constructor(private fb: FormBuilder, private baseService: BaseService) {
    this.verifyForm = this.fb.group({
      binaryValue: [''] // FormControl per il valore binario
    });
  }

  onVerify(): void {
    const binaryVerify = this.verifyForm.get('binaryValue')?.value;
    if (binaryVerify) {
      this.baseService.verifyBinaryString(binaryVerify).subscribe(
        (result: BinaryResult) => {
          this.binaryResult = result; // Salva il risultato per la visualizzazione
        },
        (error) => {
          console.error('Error verifying binary string:', error);
        }
      );
    }
  }
}
